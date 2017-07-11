import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import {RouterModule} from "@angular/router";
import { FormsModule } from "@angular/forms";
import { AppCommon } from "@app/common";
import { DefaultLayout } from "./defaultLayout";
import {MainMenu} from "./components/mainMenu"; 
@NgModule({
    imports: [CommonModule, FormsModule, RouterModule, AppCommon],
    declarations:[DefaultLayout, MainMenu]
})
export class DefaultLayoutModule { }