import {EventEmitter, Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {DOCUMENT} from "@angular/common";
import {environment} from "../../../environments/environment";
import {Observable} from "rxjs";
import {MovieResult} from "../types/MovieResult";

@Injectable()
export class MovieService {

  private _httpClient: HttpClient
  constructor(
    private http: HttpClient,
    @Inject(DOCUMENT) private document: Document
  ) {
      this._httpClient = http;
  }

  public loadMovies(): Observable<MovieResult[]> {
    const url = environment.baseUrl + 'Movie/movies';
    return this._httpClient.get<MovieResult[]>(url);
  }

  public loadShows(): Observable<MovieResult[]> {
    const url = environment.baseUrl + 'Movie/shows';
    return this._httpClient.get<MovieResult[]>(url);
  }

}
