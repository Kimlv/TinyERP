import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { BasePage, PageAction, IoCNames } from "@app/common";
import { ContentTypesModel } from "./contentTypesModel";
import { ISettingService } from "@app/setting";
import route from "../_share/config/route";
@Component({
    templateUrl: "src/modules/setting/contentType/contentTypes.html"
})
export class ContentTypes extends BasePage<ContentTypesModel> {
    constructor() {
        super();
        let self = this;
        self.model = new ContentTypesModel(self.i18nHelper);
        self.load();
        //this.model.addPageAction(new PageAction("setting.contentTypes.addNewAction", () => { self.onAddNewItemClicked(); }));
    }
    private onAddNewItemClicked() {
        this.navigate(route.contentType.addContentType.name);
    }
    public onEditItemClicked(event: any) {
        this.navigate({ name: route.contentType.editContentType.name, id: event.item.id });
    }
    public onDeleteItemClicked(event: any) {
        // let self = this;
        // settingService.deleteContentType(event.item.id).then(function () {
        //     self.load();
        // });
    }
    private load() {
        let self = this;
        let settingService: ISettingService = window.ioc.resolve(IoCNames.ISettingService);
        settingService.getContentTypes().then(function (items: Array<any>) {
            self.model.import(items);
        });
    }
}