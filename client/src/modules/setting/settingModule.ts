import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from "@angular/core";
import { ModuleConfig, ModuleNames, AppCommon, BaseModule } from "@app/common";
import { FormsModule } from "@angular/forms";
import { ContentTypes } from "./contentType/contentTypes";
import { AddOrUpdateContentType } from "./contentType/addOrUpdateContentType";
import { SettingRoute } from "./settingRoute";
import routes from "./_share/config/route";
import ioc from "./_share/config/ioc";
@NgModule({
    imports: [FormsModule, AppCommon, SettingRoute],
    declarations: [ContentTypes, AddOrUpdateContentType],
    exports: [ContentTypes],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class SettingModule extends BaseModule {
    constructor() {
        super(new ModuleConfig(ModuleNames.Security, ioc, routes));
    }
}