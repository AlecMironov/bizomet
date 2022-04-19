import { HttpClient, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { map } from 'rxjs';
import { CustomEncoder } from 'src/app/shared/custom-encoder.module';
import { UserProfileModel } from 'src/app/shared/models/user-profile.model';
import { SharedData } from 'src/app/shared/shared-data.module';
import { RepositoryService } from './repository.service';

@Injectable({
    providedIn: 'root'
})
export class ProfileService {

    constructor(
        private http: HttpClient,
        private repository: RepositoryService,
        @Inject('BASE_URL') private baseUrl: string) { }

    getProfile() {
        return this.repository.get<UserProfileModel>("userprofile/profile")
            .pipe(
                map((response) => {
                    response.picture = this.getPictureUrl(response.picture);
                    response.rolesInfo = SharedData.all_roles.filter(x => response.roles.includes(x.key)).map(x => x.name);
                    response.role = SharedData.all_roles.filter(x => x.key == response.roles[0])[0];
                    return response;
                }));
    }

    updateProfile(profile: any) {
        return this.repository.update("userprofile/update", profile);
    }

    public validatePublicName = (publicName: string) => {
        let params = new HttpParams({ encoder: new CustomEncoder() })
        params = params.append('publicName', publicName);
        return this.http.get(this.createCompleteRoute("userprofile/validateuserpublicname"), { params: params });
    }

    private getPictureUrl(data: string): string {
        if (typeof data != 'undefined' && data) {
            return `url(data:image/jpeg;base64,${data})`;
        }
        return null;
    }

    private createCompleteRoute = (route: string) => {
        return `${this.baseUrl}api/${route}`;
    }
}