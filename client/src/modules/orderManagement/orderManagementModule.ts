import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from "@angular/core";
import { ModuleConfig, ModuleNames, AppCommon, BaseModule } from "@app/common";
import { FormsModule } from "@angular/forms";
import { Orders } from "./order/orders";
import { OrderManagementRoute } from "./orderManagementRoute";
import routes from "./_share/config/route";
import ioc from "./_share/config/ioc";
@NgModule({
    imports: [FormsModule, AppCommon, OrderManagementRoute],
    declarations: [Orders],
    exports: [Orders],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class OrderManagementModule extends BaseModule {
    constructor() {
        super(new ModuleConfig(ModuleNames.Order, ioc, routes));
    }
}