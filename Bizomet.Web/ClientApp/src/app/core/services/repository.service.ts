import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'; 
import { shareReplay } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RepositoryService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  public getData<T>(route: string) {
    return this.http.get<T>(this.createCompleteRoute(route)).pipe(shareReplay(1));
  }

  public updateData(route: string, body: any) {
    return this.http.put(this.createCompleteRoute(route), body).pipe(shareReplay(1));
  }
 
  private createCompleteRoute = (route: string) => {
    return `${this.baseUrl}api/${route}`;
  }
}