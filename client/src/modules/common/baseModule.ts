import { IRouteService } from "./services/irouteService";
import { IoCNames } from "./ioc/enum";
import { ModuleConfig } from "./models/ModuleConfig";
import { IResourceManager } from "./iresourceManager";

export class BaseModule {
    protected name: string;
    protected iocConfig: Array<any> = [];
    protected routeConfig: Array<any> = [];
    constructor(config: ModuleConfig) {
        this.name = config.name;
        this.registerIoC(config.ioc);
        this.registerModuleRoutes(config.routes);
    }
    protected registerIoC(ioc: Array<any>) {
        window.ioc.import(ioc);
    }
    protected registerModuleRoutes(route: any) {
        let routeService: IRouteService = window.ioc.resolve(IoCNames.IRouteService);
        routeService.registerModuleRouteConfig(this.name, route);
    }
}