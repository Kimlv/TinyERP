import { IoCNames, BaseService, Promise, IConnector } from "@app/common";
import { IPermissionService } from "./ipermissionService";
export class PermissionService extends BaseService implements IPermissionService {
    public deletePermission(itemId: string): Promise {
        let iconnector: IConnector = window.ioc.resolve(IoCNames.IConnector);
        let url: string = String.format("permissions/{0}", itemId);
        return iconnector.delete(this.resolve(url));
    }
    public getPermissions(): Promise {
        let iconnector: IConnector = window.ioc.resolve(IoCNames.IConnector);
        return iconnector.get(this.resolve("permissions"));
    }

    public getPermission(itemId: string): Promise {
        let iconnector: IConnector = window.ioc.resolve(IoCNames.IConnector);
        let url = String.format("permissions/{0}", itemId);
        return iconnector.get(this.resolve(url));
    }

    public updatePermission(model: any): Promise {
        let iconnector: IConnector = window.ioc.resolve(IoCNames.IConnector);
        let url = String.format("permissions/{0}", model.id);
        return iconnector.put(this.resolve(url), model);
    }

    public createPermission(model: any): Promise {
        let iconnector: IConnector = window.ioc.resolve(IoCNames.IConnector);
        let url = "permissions";
        return iconnector.post(this.resolve(url), model);
    }
}