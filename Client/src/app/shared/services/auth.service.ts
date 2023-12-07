import {Inject, Injectable} from "@angular/core";
import {Observable, Subject, tap} from "rxjs";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {DOCUMENT} from "@angular/common";
import {LoginRequest} from "../types/LoginRequest";
import {LoginResult} from "../types/LoginResult";
import {SignUpRequest} from "../types/SignUpRequest";
import {SignUpResult} from "../types/SignUpResult";
import {jwtDecode} from 'jwt-decode';
import {Guid} from "../types/Guid";
import {environment} from "../../../environments/environment";


@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly tokenKey = 'token';
  private readonly userIdClaim = 'userId';
  private readonly userNameClaim =
    'https://schemas.xmlsoap.org/ws/2005/05/identity/claims/name';

  private _authStatus = new Subject<boolean>();
  public authStatus = this._authStatus.asObservable();

  constructor(
    private http: HttpClient,
    @Inject(DOCUMENT) private document: Document
  ) {}

  public isAuthenticated(): boolean {
    return this.getToken() !== null;
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }
  getUserId(): string {
    return this.getClaim(this.userIdClaim) ?? Guid.empty;
  }

  getUserName(): string {
    return this.getClaim(this.userNameClaim) ?? '';
  }

  init(): void {
    if (this.isAuthenticated()) {
      this.setAuthStatus(true);
    }
  }

  completeAuth(token: string): void {
    localStorage.setItem(this.tokenKey, token);
    this.setAuthStatus(true);
  }

  login(item: LoginRequest): Observable<LoginResult> {
    const url = environment.baseUrl + 'User/login';
    return this.http.post<LoginResult>(url, item).pipe(
      tap((loginResult: LoginResult) => {
        if (loginResult.success && loginResult.token) {
          this.completeAuth(loginResult.token);
        }
      })
    )
  }

  signup(item: SignUpRequest): Observable<SignUpResult> {
    const url = environment.baseUrl + 'User/signup';
    return this.http.post<SignUpResult>(url, item);
  }

  private setAuthStatus(isAuthenticated: boolean): void {
    this._authStatus.next(isAuthenticated);
  }

  private getClaim(claim: string): string | null {
    const token = this.getToken();
    if (token) {
      const decodedToken: any = jwtDecode(token);
      return decodedToken[claim];
    }
    return null;
  }

  logout() {
    localStorage.removeItem(this.tokenKey);
    this.setAuthStatus(false);
  }


}
