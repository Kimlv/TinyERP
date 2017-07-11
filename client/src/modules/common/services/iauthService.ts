export interface IAuthService {
    getUserProfile(): any;
    setAuth(auth: any): void;
    getAuth(): any;
    removeAuth(): void;
    isAuthenticated(profile: any):boolean;
    isAuthorized(route: any): boolean;
}