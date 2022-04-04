import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { ProjectAttachmentModel } from 'src/app/shared/models/project-attachment.model';
import { ProjectModel } from 'src/app/shared/models/project.model';
import { RepositoryService } from './repository.service';

@Injectable({
    providedIn: 'root'
})
export class ProjectService {

    constructor(private repository: RepositoryService) { }

    getAll(lazyEvent: any) {
        return this.repository.getAll<ProjectModel[]>("project", lazyEvent)
            .pipe(
                map((response) => {
                    return response;
                }));
    }

    getAllByUser(lazyEvent: any) {
        return this.repository.getAll<ProjectModel[]>("project/userprojects", lazyEvent)
            .pipe(
                map((response) => {
                    return response;
                }));
    }    

    get(id: any) {
        return this.repository.get<ProjectModel>(`project/${id}`)
            .pipe(
                map((response) => {
                    return response;
                }));
    }

    create(data: ProjectModel) {
        return this.repository.create<ProjectModel>("project", data)
            .pipe(
                map((response) => {
                    return response;
                }));
    }

    uploadAttachment(data: ProjectAttachmentModel) {
        return this.repository.create<ProjectAttachmentModel>("project/upload_attachment", data)
            .pipe(
                map((response) => {
                    return response;
                }));
    }

    update(data: ProjectModel) {
        return this.repository.update<ProjectModel>(`project/${data.id}`, data)
            .pipe(
                map((response) => {
                    return response;
                }));
    }

    delete(id: any) {
        return this.repository.delete(`project/${id}`);
    }
}