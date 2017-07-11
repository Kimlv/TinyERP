import { Component, Input } from "@angular/core";
import { AppMenuItem } from "@app/common";
@Component({
    selector: "main-menu",
    templateUrl: "src/themes/default/components/mainMenu.html"
})
export class MainMenu {
    @Input() cls: string = "nav side-menu";
    @Input() items: Array<AppMenuItem>;
}