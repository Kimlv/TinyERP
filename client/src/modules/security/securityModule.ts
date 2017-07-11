import { NgModule, CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA, ApplicationRef } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";
import { ModuleConfig, ModuleNames, AppCommon, BaseModule } from "@app/common";
import { SecurityRoute } from "./securityRoute";
import routes from "./_share/config/route";
import ioc from "./_share/config/ioc";

/*Permisison */
import { Permissions } from "./permission/permissions";
import { AddOrUpdatePermission } from "./permission/addOrUpdatePermission";

@NgModule({
    imports: [CommonModule, FormsModule, AppCommon, SecurityRoute],
    declarations: [AddOrUpdatePermission, Permissions],
    schemas: [CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA]
})
export class SecurityModule extends BaseModule {
    constructor() {
        super(new ModuleConfig(ModuleNames.Security, ioc, routes));
    }
}