import { Injectable } from '@angular/core';
import { map, of } from 'rxjs';
import { UserProfileModel } from 'src/app/shared/models/user-profile.model';
import { RepositoryService } from './repository.service';

export enum PictureSize {
    Small = 0,
    Large = 1
}

const USER_PROFILE_KEY = 'user-profile';

@Injectable({
    providedIn: 'root'
})
export class ProfileService {

    constructor(private repository: RepositoryService) { }

    getProfile() {
        let storedProfile = this.getStoredProfile();
        if (storedProfile)
            return of(storedProfile);

        return this.repository.getData<UserProfileModel>("userprofile/profile")
            .pipe(
                map((response) => {
                    response.pictureLarge = this.getPictureUrl(response.pictureLarge);
                    response.pictureSmall = this.getPictureUrl(response.pictureSmall);
                    this.saveUserProfile(response);
                    return response;
                }));
    }

    getProfilePicture(size: PictureSize) {
        let storedProfile = this.getStoredProfile();
        if (storedProfile) {
            return size == PictureSize.Large ? of(storedProfile.pictureLarge) : of(storedProfile.pictureSmall);
        }

        return this.repository.getData(`userprofile/profileimage?large=${size == PictureSize.Large ? 'true' : 'false'}`)
            .pipe(
                map((response: any) => {
                    return this.getPictureUrl(response.result);
                }));
    }

    updateProfile(profile: UserProfileModel): void {
        this.saveUserProfile(profile);
    }

    private getPictureUrl(data: string): string {
        if (typeof data != 'undefined' && data) {
            return `url(data:image/jpeg;base64,${data})`;
        }
        return null;
    }

    private getStoredProfile(): UserProfileModel | null {
        const profile = localStorage.getItem(USER_PROFILE_KEY);
        if (profile) {
            return JSON.parse(profile);
        }

        return null;
    }

    private saveUserProfile(profile: UserProfileModel): void {
        localStorage.removeItem(USER_PROFILE_KEY);
        if (typeof profile != 'undefined' && profile)
            localStorage.setItem(USER_PROFILE_KEY, JSON.stringify(profile));
    }
}