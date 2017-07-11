import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { ContentTypes } from "./contentType/contentTypes";
import { AddOrUpdateContentType } from "./contentType/addOrUpdateContentType";
import route from "./_share/config/route";

let routeConfigs: Routes = [
    { path: "", redirectTo: route.contentType.contentTypes.path, pathMatch: "full" },
    { path: route.contentType.contentTypes.path, component: ContentTypes },
    { path: route.contentType.addContentType.path, component: AddOrUpdateContentType },
    { path: route.contentType.editContentType.path, component: AddOrUpdateContentType }
];

@NgModule({
    imports: [RouterModule.forChild(routeConfigs)],
    exports: [RouterModule],

})
export class SettingRoute { }