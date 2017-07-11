import { Router, ActivatedRoute } from "@angular/router";
import { Component } from "@angular/core";
import { BasePage, FormMode, IoCNames } from "@app/common";
import { AddOrUpdatePermissionModel } from "./addOrUpdatePermissionModel";
import { IPermissionService } from "@app/security";
import route from "../_share/config/route";

@Component({
    templateUrl: "src/modules/security/permission/addOrUpdatePermission.html"
})
export class AddOrUpdatePermission extends BasePage<AddOrUpdatePermissionModel> {
    private mode: FormMode = FormMode.AddNew;
    private itemId: any;
    constructor(router: Router, activatedRoute: ActivatedRoute) {
        super();
        let self = this;
        self.model = new AddOrUpdatePermissionModel();
        if (!!activatedRoute.params["value"].id) {
            self.mode = FormMode.Edit;
            self.itemId = activatedRoute.params["value"].id;
            let permissionService: IPermissionService = window.ioc.resolve(IoCNames.IPermissionService);
            permissionService.getPermission(self.itemId).then(function (item: any) {
                self.model.import(item);
            });
        }
    }
    public onSaveClicked(event: any): void {
        let self = this;
        let permissionService: IPermissionService = window.ioc.resolve(IoCNames.IPermissionService);
        if (!this.model.validated()) { return; }
        if (self.mode === FormMode.AddNew) {
            permissionService.createPermission(this.model).then(function () {
                self.navigate(route.permission.permissions.name);
            });
            return;
        }
        permissionService.updatePermission(this.model).then(function () {
            self.navigate(route.permission.permissions.name);
        });
    }
    public onCancelClicked(event: any): void {
        let self = this;
        self.navigate(route.permission.permissions.name);
    }
}