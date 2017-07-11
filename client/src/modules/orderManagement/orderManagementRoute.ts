import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { Orders } from "./order/orders";
import route from "./_share/config/route";

let routeConfigs: Routes = [
    { path: "", redirectTo: route.order.orders.path, pathMatch: "full" },
    { path: route.order.orders.path, component: Orders }
];

@NgModule({
    imports: [RouterModule.forChild(routeConfigs)],
    exports: [RouterModule],

})
export class OrderManagementRoute { }