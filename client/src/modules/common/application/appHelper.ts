import { IAppConfig } from "./iappConfig";
import { IInjector } from "../ioc/iinjector";

class AppHelper {
    public injector: IInjector = null;
    public config: IAppConfig = null;

    public getConfig(): any {
        if (!this.config) {
            throw "Config was not set in appHelper";
        }
        return this.config;
    }
    public setConfig(config: IAppConfig) {
        this.config = config;
    }
    public setInjector(injector: IInjector) {
        this.injector = injector;
    }

}

let appHelper = new AppHelper();
export default appHelper;