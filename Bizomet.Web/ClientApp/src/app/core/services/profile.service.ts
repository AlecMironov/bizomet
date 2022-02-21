import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { UserProfileModel } from 'src/app/shared/models/user-profile.model';
import { SharedData } from 'src/app/shared/shared-data.module';
import { RepositoryService } from './repository.service';

@Injectable({
    providedIn: 'root'
})
export class ProfileService {

    constructor(private repository: RepositoryService) { }

    getProfile() {
        return this.repository.get<UserProfileModel>("userprofile/profile")
            .pipe(
                map((response) => {
                    response.picture = this.getPictureUrl(response.picture);
                    response.rolesInfo = SharedData.all_roles.filter(x => response.roles.includes(x.key)).map(x => x.name);
                    return response;
                }));
    }

    updateProfile(profile: any) {
        return this.repository.update("userprofile/update", profile);
    }

    private getPictureUrl(data: string): string {
        if (typeof data != 'undefined' && data) {
            return `url(data:image/jpeg;base64,${data})`;
        }
        return null;
    }
}