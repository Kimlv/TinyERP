import { OnInit, AfterContentInit, AfterViewInit, AfterViewChecked, OnDestroy, OnChanges } from "@angular/core";
import { Hashtable } from "../list/hashtable";
import { AuthenticatedEvent, ApplicationStateEvent } from "../../enum";
import { IConnector } from "../../connectors/iconnector";
import { IResourceManager } from "../../iresourceManager";
import { IEventManager } from "../../event";
import { ComponentType } from "./enum";
import { AuthenticationMode } from "./enum";
import { IoCNames } from "../../ioc/enum";

import guidHelper from "../../helpers/guidHelper";
import authService from "../../services/authService";
export class BaseComponent implements OnInit, AfterContentInit, AfterViewInit, OnDestroy, OnChanges {
    protected eventManager: IEventManager;
    protected events: Hashtable<any>;
    public i18n: any;
    protected i18nHelper: IResourceManager;
    public id: string = guidHelper.create();
    constructor(componentType: any = ComponentType.Layout) {
        this.eventManager = window.ioc.resolve(IoCNames.IEventManager);
        let resourceManager: IResourceManager = window.ioc.resolve(IoCNames.IResource);
        this.i18nHelper = resourceManager;
        this.i18n = resourceManager.getResourceData();
        this.events = new Hashtable<any>();
    }
    ngOnInit() {
        this.onInit();
        let self: BaseComponent = this;
        this.events.getKeys().forEach(function (key) {
            let handler: any = self.events.get(key);
            self.eventManager.subscribe(key, handler);
        });
    }
    protected onInit() {
    }

    ngAfterContentInit() {
        this.onBeforeReady();
    }
    protected onBeforeReady() {
    }

    ngAfterViewInit() {
        this.onReady();
    }
    protected onReady() {
    }

    ngOnChanges() {
        this.onChange();
    }
    protected onChange() { }

    ngOnDestroy() {
        let self: BaseComponent = this;
        this.events.getKeys().forEach(function (key) {
            self.eventManager.unsubscribe(key);
        });
        this.onUnload();
    }
    protected onUnload() {
    }
    
    protected setResources(resources: Array<string>) {
        let resourceHelper: IResourceManager = window.ioc.resolve(IoCNames.IResource);
        resourceHelper.load(resources);
    }
    
    public registerEvent(key: string, handler: any): void {
        this.events.set(key, handler);
    }
}