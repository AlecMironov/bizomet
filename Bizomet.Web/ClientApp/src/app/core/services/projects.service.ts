import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { UserPortfolioModel } from 'src/app/shared/models/user-portfolio.model';
import { RepositoryService } from './repository.service';

@Injectable({
    providedIn: 'root'
})
export class ProjectsService {

    constructor(private repository: RepositoryService) { }

    getAll(lazyEvent: any) {
        return this.repository.getAll<UserPortfolioModel[]>("userportfolio", lazyEvent)
            .pipe(
                map((response) => {
                    return response;
                }));
    }

    get(id: any) {
        return this.repository.get<UserPortfolioModel>(`userportfolio/${id}`)
            .pipe(
                map((response) => {
                    return response;
                }));
    }

    create(data: UserPortfolioModel) {
        return this.repository.create<UserPortfolioModel>("userportfolio", data)
            .pipe(
                map((response) => {
                    return response;
                }));
    }

    update(data: UserPortfolioModel) {
        return this.repository.update<UserPortfolioModel>(`userportfolio/${data.id}`, data)
            .pipe(
                map((response) => {
                    return response;
                }));
    }

    delete(id: any) {
        return this.repository.delete(`userportfolio/${id}`);
    }
}