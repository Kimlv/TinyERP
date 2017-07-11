import { Component } from "@angular/core";
import { AppMenuItem, BaseLayout } from "@app/common";
import helperFacade from "@app/common";

@Component({
    selector: "layout",
    templateUrl: "src/themes/default/defaultLayout.html"
})
export class DefaultLayout extends BaseLayout {
    public menuItems: Array<AppMenuItem> = [];
    protected onBeforeReady() {
        console.log(helperFacade.appHelper.getConfig().getMainMenus);
        this.menuItems = helperFacade.appHelper.getConfig().menus;
        console.log("menus in layout:", this.menuItems);
    }
}