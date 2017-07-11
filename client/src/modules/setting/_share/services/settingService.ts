import { IConnector, IoCNames, PromiseFactory, Promise, BaseService } from "@app/common";
import { ISettingService } from "./isettingService";
export class SettingService extends BaseService implements ISettingService {
    private iconnector: IConnector;
    public getContentTypes(): Promise {
        let iconnector: IConnector = window.ioc.resolve(IoCNames.IConnector);
        return iconnector.get(this.resolve("contentTypes"));
    }
    public getContentType(id: string): Promise {
        let iconnector: IConnector = window.ioc.resolve(IoCNames.IConnector);
        let url = String.format("contentTypes/{0}", id);
        return iconnector.get(this.resolve(url));
    }

    public createContentType<TContent>(model: TContent): Promise {
        console.log(model)
        let iconnector: IConnector = window.ioc.resolve(IoCNames.IConnector);
        let url = "contentTypes";
        return iconnector.post(this.resolve(url), model);
    }

    public updateContentType<TContent>(model: TContent): Promise {
        console.log(model);
        let iconnector: IConnector = window.ioc.resolve(IoCNames.IConnector);
        let url = "contentTypes";
        return iconnector.put(this.resolve(url), model);
    }
}