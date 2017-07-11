import { Promise } from "@app/common";
export interface IOrderService {
    getOrders(): Promise;
}