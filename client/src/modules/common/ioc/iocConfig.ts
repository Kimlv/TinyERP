import { IoCNames, IoCLifeCycle } from "./enum";
import { HttpConnector } from "../connectors/httpConnector";
import { ConsoleLogger } from "../services/logger/consoleLogger";
import { EventManager } from "../event/eventManager";
import { ResourceManager } from "../resourceManager";
import { CacheService } from "../services/cache/cacheService";
import { RouteService } from "../services/routeService";
let ioc = [
    { name: IoCNames.IConnector, instance: HttpConnector, lifeCycle: IoCLifeCycle.Singleton },
    { name: IoCNames.ILogger, instance: ConsoleLogger, lifeCycle: IoCLifeCycle.Singleton },
    { name: IoCNames.IEventManager, instance: EventManager, lifeCycle: IoCLifeCycle.Singleton },
    { name: IoCNames.IResource, instance: ResourceManager, lifeCycle: IoCLifeCycle.Singleton },
    { name: IoCNames.ICacheService, instance: CacheService, lifeCycle: IoCLifeCycle.Singleton },
    { name: IoCNames.IRouteService, instance: RouteService, lifeCycle: IoCLifeCycle.Singleton }
];
export default ioc;