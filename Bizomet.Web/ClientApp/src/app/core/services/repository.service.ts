import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'; 
import { shareReplay } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RepositoryService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  public getAll<T>(route: string, options: any) {
    return this.http.get<T>(this.createCompleteRoute(route), { params: options }).pipe(shareReplay(1));
  }

  public get<T>(route: string) {
    return this.http.get<T>(this.createCompleteRoute(route)).pipe(shareReplay(1));
  }

  public create<T>(route: string, body: T) {
    return this.http.post<T>(this.createCompleteRoute(route), body).pipe(shareReplay(1));
  }

  public update<T>(route: string, body: T) {
    return this.http.put<T>(this.createCompleteRoute(route), body).pipe(shareReplay(1));
  }
 
  public delete(route: string) {
    return this.http.delete(this.createCompleteRoute(route)).pipe(shareReplay(1));
  }

  private createCompleteRoute = (route: string) => {
    return `${this.baseUrl}api/${route}`;
  }
}