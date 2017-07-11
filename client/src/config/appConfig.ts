import { IAppConfig, LANG, Locale } from "@app/common";
import { DefaultLayoutModule, DefaultLayout } from "@app/themes/default";
import helperFacade from "@app/common";
import modules from "./modules";
import ioc from "./ioc";
import menus from "./menus";
let appConfig: IAppConfig = {
    menus: menus,
    localization: {
        lang: LANG.EN,
        /* files: [Locale.Common, Locale.Setting, Locale.Security] */
        files: helperFacade.moduleHelper.getModuleLocales(modules)
    },
    localeUrl: "/src/resources/locales/",
    rootApi: "http://localhost:22383/api/",
    ioc: ioc,
    layout: {
        component: DefaultLayout,
        module: DefaultLayoutModule
    },
    routes: helperFacade.routeHelper.getModuleRoute(modules)
}
export default appConfig