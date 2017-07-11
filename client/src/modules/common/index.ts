import { HelperFacade } from "./models/helperFacade";

export * from "./commonModule";
export * from "./exception";
export * from "./enum";
export * from "./baseModule";
export * from "./resourceManager";
export * from "./iresourceManager";

export * from "./ioc/enum";

export * from "./connectors/httpConnector";
export * from "./connectors/iconnector";

export * from "./models/promise";
export * from "./models/ui";
export * from "./models/baseModel";
export * from "./models/menu/appMenuItem";
export * from "./models/moduleConfig";
export * from "./models/imoduleConfigItem";
export * from "./models/ui/baseLayout";

export * from "./application/iappConfig";

export * from "./services/logger/consoleLogger";
export * from "./services/baseService";
export * from "./services/cache/cacheService";
export * from "./services/routeService";

export * from "./event/eventManager";

import iocHelper from "./ioc/iocHelper";
import appHelper from "./application/appHelper";
import routerHelper from "./helpers/routerHelper";
import domHelper from "./helpers/domHelper";
import moduleHelper from "./helpers/moduleHelper";
import urlHelper from "./helpers/urlHelper";

let helperFacade: HelperFacade = new HelperFacade();
helperFacade.iocHelper = iocHelper;
helperFacade.appHelper = appHelper;
helperFacade.routeHelper = routerHelper;
helperFacade.domHelper = domHelper;
helperFacade.moduleHelper = moduleHelper;
helperFacade.urlHelper = urlHelper;

export default helperFacade;
