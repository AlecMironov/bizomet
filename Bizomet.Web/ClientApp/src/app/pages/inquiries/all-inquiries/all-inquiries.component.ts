import { Component, OnInit } from '@angular/core';
import { AppBreadcrumbService } from 'src/app/core/services/app.breadcrumb.service';
import { InquiryModel } from 'src/app/shared/models/inquiry.model';
import { LazyLoadEvent } from 'primeng/api';
import { InquiryService } from 'src/app/core/services/inquiry.service';

@Component({
  selector: 'app-all-inquiries',
  templateUrl: './all-inquiries.component.html'
})
export class AllInquiriesComponent implements OnInit {

  loading: boolean;
  inquiryList: InquiryModel[];
  inquiry: InquiryModel;
  totalRecords: number = 0;

  constructor(private inquiryService: InquiryService, private breadcrumbService: AppBreadcrumbService) {
    this.breadcrumbService.setItems([
      { label: 'Inquiries', icon: 'pi pi-fw pi-home mr-1' },
      { label: 'All Inquiries' }
    ]);
  }

  ngOnInit(): void {
  }

  loadData(event: LazyLoadEvent) {
    this.loading = true;

    this.inquiryService.getAll(event)
      .subscribe((res: any) => {
        this.inquiryList = res.data;
        this.totalRecords = res.total_records;
        this.loading = false;
      });
  }
}
