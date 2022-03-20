import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { InquiryModel } from 'src/app/shared/models/inquiry.model';
import { RepositoryService } from './repository.service';

@Injectable({
    providedIn: 'root'
})
export class InquiryService {

    constructor(private repository: RepositoryService) { }

    getAll(lazyEvent: any) {
        return this.repository.getAll<InquiryModel[]>("inquiry", lazyEvent)
            .pipe(
                map((response) => {
                    return response;
                }));
    }

    getUserInquiries(lazyEvent: any) {
        return this.repository.getAll<InquiryModel[]>("inquiry/userinquiries", lazyEvent)
            .pipe(
                map((response) => {
                    return response;
                }));
    }

    get(id: any) {
        return this.repository.get<InquiryModel>(`inquiry/${id}`)
            .pipe(
                map((response) => {
                    return response;
                }));
    }

    create(data: InquiryModel) {
        return this.repository.create<InquiryModel>("inquiry", data)
            .pipe(
                map((response) => {
                    return response;
                }));
    }

    update(data: InquiryModel) {
        return this.repository.update<InquiryModel>(`inquiry/${data.id}`, data)
            .pipe(
                map((response) => {
                    return response;
                }));
    }

    delete(id: any) {
        return this.repository.delete(`inquiry/${id}`);
    }
}