import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { Permissions } from "./permission/permissions";
import { AddOrUpdatePermission } from "./permission/addOrUpdatePermission";
import routes from "./_share/config/route";

let routeConfigs: Routes = [
    { path: "", redirectTo: routes.permission.permissions.path, pathMatch: "full" },
    { path: routes.permission.permissions.path, component: Permissions },
    { path: routes.permission.addPermission.path, component: AddOrUpdatePermission },
    { path: routes.permission.editPermission.path, component: AddOrUpdatePermission }
];
@NgModule({
    imports: [RouterModule.forChild(routeConfigs)],
    exports: [RouterModule]
})
export class SecurityRoute { }