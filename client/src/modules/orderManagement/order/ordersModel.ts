import { IoCNames } from "@app/common";
export class OrdersModel {
    public options: any = {};
    public eventKey: string = "orders_ondatasource_changed";
    constructor(resourceHelper: any) {
        this.options = {
            columns: [
                { field: "name", title: resourceHelper.resolve("orderManagement.orders.grid.name") },
                { field: "totalItems", title: resourceHelper.resolve("orderManagement.orders.grid.totalItems") },
                { field: "totalPrice", title: resourceHelper.resolve("orderManagement.orders.grid.totalPrice") },
                { field: "createdDate", title: resourceHelper.resolve("orderManagement.orders.grid.createdDate") }
            ],
            data: [],
            enableEdit: false,
            enableDelete: false
        };
    }
    public import(items: Array<any>) {
        let eventManager = window.ioc.resolve(IoCNames.IEventManager);
        eventManager.publish(this.eventKey, items);
    }
}