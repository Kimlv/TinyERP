import { IRouteService } from "../services/irouteService";
//import { IModuleConfig } from "../models/imoduleConfig";
import {IModuleConfigItem} from "../models/imoduleConfigItem";
import { IoCNames } from "../ioc/enum";
import gcHelper from "./gcHelper";

const MODULE_PATTERN = "src/modules/{0}/{0}Module#{1}Module";
const ROUTE_PARAMETER_PREFIX = ":";
const ROUTE_SEGTMENT_SEPARATOR = "/";

let routeHelper = {
    resolveUrl: resolveUrl,
    getModuleRoute: getModuleRoute
};
export default routeHelper;

/*
 options:{
     name:meaningful name of route in format <module name>.<sub feature name>.<page name>, such as: security.contentType.addNewContentType,
     param1: name of parameter, such as id
 } 
 options={name:"security.contentType.editContentType", id: 1} can be resolved as "/security/contentTypes/editContentType/1". This belong to the path pattern of each module. Please have  alook for more default in <module>Module.ts file
 */
function resolveUrl(options: any): string {
    if (!options || (window.Sys.isObject(options) && String.isNullOrWhiteSpace(options.name))) {
        throw "Please specify valid value for options parameter.";
    }
    let name = window.Sys.isString(options) ? options : options.name;
    gcHelper.collect(options, "name");
    let routeService: IRouteService = window.ioc.resolve(IoCNames.IRouteService);
    let route = routeService.getByRouteName(name);
    let path = String.format("{0}{1}{2}", name.split(".")[0], ROUTE_SEGTMENT_SEPARATOR, route.path);
    return resolveUrlFromPath(path, options);
}
function resolveUrlFromPath(path: string, options: any): string {
    let segments = path.split(ROUTE_SEGTMENT_SEPARATOR);
    for (let itemIndex = 0; itemIndex < segments.length; itemIndex++) {
        if (!segments[itemIndex].startsWith(ROUTE_PARAMETER_PREFIX)) { continue; }
        let parameterName = segments[itemIndex].replace(ROUTE_PARAMETER_PREFIX, "");
        if (!options[parameterName] || String.isNullOrWhiteSpace(options[parameterName])) {
            throw String.format("\Missing value for \'{0}\' parameter when resolve \'{1}\' path.", segments[itemIndex], path);
        }
        segments[itemIndex] = options[parameterName];
    }
    return segments.toString(ROUTE_SEGTMENT_SEPARATOR);
}
function getModuleRoute(modules: Array<IModuleConfigItem>) {
    let routes: Array<any> = [];
    let defaultModule: IModuleConfigItem = null;
    modules.forEach((module: IModuleConfigItem) => {
        routes.push({ path: module.urlPrefix, loadChildren: getModuleFullPath(module.path) });
        if (module.isDefault && module.isDefault == true) {
            defaultModule = module;
        }
    });
    if (defaultModule != null) {
        routes.push({ path: "", redirectTo: defaultModule.urlPrefix, pathMatch: "full" });
    }
    return routes;
}

function getModuleFullPath(moduleName: string) {
    return String.format(MODULE_PATTERN, moduleName, String.toPascalCase(moduleName))
}