import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { BasePage, PageAction, IoCNames, PageDropdownAction } from "@app/common";
import { PermissionsModel } from "./permissionsModel";
import { IPermissionService } from "@app/security";
import route from "../_share/config/route";
@Component({
    templateUrl: "src/modules/security/permission/permissions.html"
})
export class Permissions extends BasePage<PermissionsModel> {
    constructor() {
        super();
        let self = this;
        self.model = new PermissionsModel(self.i18nHelper);
        self.load();
        this.model.addPageAction(new PageAction("security.permissions.addNewAction", () => { self.onAddNewItemClicked(); }));
    }
    private onAddNewItemClicked() {
        this.navigate(route.permission.addPermission.name);
    }
    public onEditItemClicked(event: any) {
        this.navigate({ name: route.permission.editPermission.name, id: event.item.id });
    }
    public onDeleteItemClicked(event: any) {
        let self = this;
        let permissionService: IPermissionService = window.ioc.resolve(IoCNames.IPermissionService);
        permissionService.deletePermission(event.item.id).then(function () {
            self.load();
        });
    }
    private load() {
        let self = this;
        let permissionService: IPermissionService = window.ioc.resolve(IoCNames.IPermissionService);
        permissionService.getPermissions().then(function (items: Array<any>) {
            self.model.import(items);
        });
    }
}