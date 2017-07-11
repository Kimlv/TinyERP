import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { BasePage, PageAction, IoCNames } from "@app/common";
import { OrdersModel } from "./ordersModel";
import { IOrderService } from "../_share/services/iorderService";
import route from "../_share/config/route";
@Component({
    templateUrl: "src/modules/orderManagement/order/orders.html"
})
export class Orders extends BasePage<OrdersModel> {
    constructor() {
        super();
        let self = this;
        self.model = new OrdersModel(self.i18nHelper);
        self.load();
    }
    private load() {
        let self = this;
        let orderService: IOrderService = window.ioc.resolve(IoCNames.IOrderService);
        orderService.getOrders().then(function (items: Array<any>) {
            self.model.import(items);
        });
    }
}