import { Component, OnInit } from '@angular/core';
import { AppBreadcrumbService } from 'src/app/core/services/app.breadcrumb.service';
import { InquiryModel } from 'src/app/shared/models/inquiry.model';
import { InquiryService } from 'src/app/core/services/inquiry.service';
import { LazyLoadEvent } from 'primeng/api';

@Component({
  selector: 'app-my-pitches',
  templateUrl: './my-pitches.component.html'
})
export class MyPitchesComponent implements OnInit {

  loading: boolean;
  inquiryList: InquiryModel[];
  inquiry: InquiryModel;
  totalRecords: number = 0;

  constructor(private inquiryService: InquiryService, private breadcrumbService: AppBreadcrumbService) {
    this.breadcrumbService.setItems([
      { label: 'Inquiries', icon: 'pi pi-fw pi-home mr-1' },
      { label: 'My Inquiries' }
    ]);
  }

  ngOnInit(): void {
  }

  loadData(event: LazyLoadEvent) {
    this.loading = true;

    this.inquiryService.getUserInquiries(event)
      .subscribe((res: any) => {
        this.inquiryList = res.data;
        this.totalRecords = res.total_records;
        this.loading = false;
      });
  }
}
