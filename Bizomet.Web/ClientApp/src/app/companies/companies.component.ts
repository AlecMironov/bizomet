import { Component, Inject } from '@angular/core';
import { Company } from '../shared/models/company.models';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-companies',
  templateUrl: './companies.component.html',
  styleUrls: ['./companies.component.css']
})
export class CompaniesComponent {
  public companies: Company[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Company[]>(baseUrl + 'api/companies').subscribe(result => {
      this.companies = result;
    }, error => console.error(error));
  }
}
