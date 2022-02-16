import { Injectable } from '@angular/core';
import { map, of } from 'rxjs';
import { UserProfileModel } from 'src/app/shared/models/user-profile.model';
import { RepositoryService } from './repository.service';

export enum PictureSize {
    Small = 0,
    Large = 1
}

@Injectable({
    providedIn: 'root'
})
export class ProfileService {

    constructor(private repository: RepositoryService) { }

    getProfile() {
        return this.repository.getData<UserProfileModel>("userprofile/profile")
            .pipe(
                map((response) => {
                    response.picture = this.getPictureUrl(response.picture);
                    return response;
                }));
    }

    updateProfile(profile: any) {
        // if (typeof profile.pictureLarge != 'undefined' && profile.pictureLarge) {
        //     profile.pictureLarge = profile.pictureLarge.substring(4, profile.pictureLarge.length - 1);
        // }
        //let model = JSON.stringify(profile);
        return this.repository.updateData("userprofile/update", profile);
    }

    private getPictureUrl(data: string): string {
        if (typeof data != 'undefined' && data) {
            return `url(data:image/jpeg;base64,${data})`;
        }
        return null;
    }
}