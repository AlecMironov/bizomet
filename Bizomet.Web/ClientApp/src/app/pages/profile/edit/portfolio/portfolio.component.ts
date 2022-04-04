import { Component, OnInit } from '@angular/core';
import { AppBreadcrumbService } from 'src/app/core/services/app.breadcrumb.service';
import { ConfirmationService, LazyLoadEvent, MessageService } from 'primeng/api';

import { UserPortfolioModel } from 'src/app/shared/models/user-portfolio.model';
import { PortfolioService } from 'src/app/core/services/portfolio.service';

@Component({
  selector: 'app-portfolio',
  templateUrl: './portfolio.component.html',
})
export class ProfilePortfolioComponent implements OnInit {

  portfolioDialog: boolean = false;
  portfolioList: UserPortfolioModel[];
  portfolio: UserPortfolioModel;
  submitted: boolean = false;
  loading: boolean = false;
  totalRecords: number = 0;

  constructor(
    private portfolioService: PortfolioService,
    private breadcrumbService: AppBreadcrumbService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService) {

    this.breadcrumbService.setItems([
      { label: 'Profile', routerLink: ['/profile'], icon: 'pi pi-fw pi-user mr-1' },
      { label: 'Portfolio' }
    ]);
  }

  ngOnInit(): void {
  }

  loadPortfolio(event: LazyLoadEvent) {
    this.loading = true;

    this.portfolioService.getAll(event)
      .subscribe((res: any) => {
        this.portfolioList = res.data;
        this.totalRecords = res.total_records;
        this.loading = false;
      });
  }

  openNew() {
    this.portfolio = new UserPortfolioModel();
    this.submitted = false;
    this.portfolioDialog = true;
  }

  editPortfolio(portfolio: UserPortfolioModel) {
    this.portfolio = { ...portfolio };
    this.portfolioDialog = true;
  }

  deletePortfolio(portfolio: UserPortfolioModel) {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete ' + portfolio.title + '?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.portfolioService.delete(portfolio.id).subscribe(result => {
          this.portfolioList = this.portfolioList.filter(val => val.id !== portfolio.id);
          this.portfolio = new UserPortfolioModel();
          this.totalRecords = this.totalRecords - 1;
          this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Portfolio Reference Deleted', life: 3000 });
        });
      }
    });
  }

  hideDialog() {
    this.portfolioDialog = false;
    this.submitted = false;
  }

  saveProduct() {
    this.submitted = true;

    if (this.portfolio.title.trim()) {
      if (this.portfolio.id) {
        this.portfolioService.update(this.portfolio).subscribe(result => {
          this.portfolio = result;
          this.portfolioList[this.findIndexById(this.portfolio.id)] = this.portfolio;
          this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Portfolio Reference Updated', life: 3000 });
        });
      }
      else {
        this.portfolio.id = "newId";
        this.portfolioService.create(this.portfolio).subscribe(result => {
          this.portfolio = result;
          this.portfolioList.push(this.portfolio);
          this.totalRecords = this.totalRecords + 1;
          this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'New Reference Created', life: 3000 });
        });
      }

      this.portfolioList = [...this.portfolioList];
      this.portfolioDialog = false;
      this.portfolio = new UserPortfolioModel();
    }
  }

  findIndexById(id: string): number {
    let index = -1;
    for (let i = 0; i < this.portfolioList.length; i++) {
      if (this.portfolioList[i].id === id) {
        index = i;
        break;
      }
    }

    return index;
  }
}
