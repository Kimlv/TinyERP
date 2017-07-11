import { IoCNames, IoCLifeCycle } from "@app/common";
import { OrderService } from "../services/orderService";
let ioc = [
    { name: IoCNames.IOrderService, instance: OrderService, lifeCycle: IoCLifeCycle.Singleton }
];
export default ioc;