import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'; 

@Injectable({
  providedIn: 'root'
})
export class RepositoryService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  public getData<T>(route: string) {
    return this.http.get<T>(this.createCompleteRoute(route));
  }
 
  private createCompleteRoute = (route: string) => {
    return `${this.baseUrl}api/${route}`;
  }
}