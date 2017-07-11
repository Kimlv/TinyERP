import { Promise } from "@app/common";
export interface ISettingService {
    getContentTypes(): Promise;
    getContentType(id:string): Promise;
    createContentType<TContent>(content: TContent): Promise;
    updateContentType<TContent>(content: TContent): Promise;
}