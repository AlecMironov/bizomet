import { Component, OnInit } from '@angular/core';
import { AppBreadcrumbService } from 'src/app/core/services/app.breadcrumb.service';
import { RepositoryService } from 'src/app/core/services/repository.service';
import { InquiryModel } from 'src/app/shared/models/inquiry.model';
import { LazyLoadEvent } from 'primeng/api';

@Component({
  selector: 'app-all-inquiries',
  templateUrl: './all-inquiries.component.html'
})
export class AllInquiriesComponent implements OnInit {

  loading: boolean;
  inquiryList: InquiryModel[];
  inquiry: InquiryModel;
  totalRecords: number = 0;

  constructor(private repositoryService: RepositoryService, private breadcrumbService: AppBreadcrumbService) {
    this.breadcrumbService.setItems([
      { label: 'Inquiries', icon: 'pi pi-fw pi-home mr-1' },
      { label: 'All Inquiries' }
    ]);
  }

  ngOnInit(): void {
    this.loading = true;

    this.repositoryService.getAll<any>("common/contactusreason", null)
      .subscribe(res => {
        this.loading = false;
      });
  }

  loadData(event: LazyLoadEvent) {
    // this.loading = true;

    // this.portfolioService.getAll(event)
    //   .subscribe((res: any) => {
    //     this.portfolioList = res.data;
    //     this.totalRecords = res.total_records;
    //     this.loading = false;
    //   });
  }
}
