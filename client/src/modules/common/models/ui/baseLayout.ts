import { ComponentType } from "./enum";
import { BaseComponent } from "./baseComponent";
export class BaseLayout extends BaseComponent {
    constructor() {
        super(ComponentType.Layout)
    }
}