import { IConnector, IoCNames, Promise, BaseService } from "@app/common";
import { IOrderService } from "./iorderService";
export class OrderService extends BaseService implements IOrderService {
    private iconnector: IConnector;
    public getOrders(): Promise {
        let iconnector: IConnector = window.ioc.resolve(IoCNames.IConnector);
        return iconnector.get(this.resolve("orders"));
    }
}