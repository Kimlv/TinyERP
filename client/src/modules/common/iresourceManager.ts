export interface IResourceManager {
    resolve(key: string): string;
    load(modules: Array<string>): void;
    getResourceData(): any;
}