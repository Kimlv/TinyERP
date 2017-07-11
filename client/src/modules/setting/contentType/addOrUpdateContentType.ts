import { Router, ActivatedRoute } from "@angular/router";
import { Component } from "@angular/core";
import { BasePage, FormMode, IoCNames } from "@app/common";
import { AddOrUpdateContentTypeModel } from "./addOrUpdateContentTypeModel";
import { ISettingService } from "@app/setting";
import route from "../_share/config/route";

@Component({
    templateUrl: "src/modules/setting/contentType/addOrUpdateContentType.html"
})
export class AddOrUpdateContentType extends BasePage<AddOrUpdateContentTypeModel> {
    private mode: FormMode = FormMode.AddNew;
    private itemId: any;
    constructor(activatedRoute: ActivatedRoute) {
        super();
        let self = this;
        self.model = new AddOrUpdateContentTypeModel();
        if (!!activatedRoute.params["value"].id) {
            self.mode = FormMode.Edit;
            self.itemId = activatedRoute.params["value"].id;
            let settingService: ISettingService = window.ioc.resolve(IoCNames.ISettingService);
            settingService.getContentType(self.itemId).then(function (item: any) {
                self.model.import(item);
            });
        }
    }
    public onSaveClicked(event: any): void {
        let self = this;
        let settingService: ISettingService = window.ioc.resolve(IoCNames.ISettingService);
        if (!this.model.validated()) { return; }
        if (self.mode === FormMode.AddNew) {
            settingService.createContentType(this.model).then(function () {
                self.navigate(route.contentType.contentTypes.name);
            });
            return;
        }
        settingService.updateContentType(this.model).then(function () {
            self.navigate(route.contentType.contentTypes.name);
        });
    }
    public onCancelClicked(event: any): void {
        let self = this;
        self.navigate(route.contentType.contentTypes.name);
    }
}