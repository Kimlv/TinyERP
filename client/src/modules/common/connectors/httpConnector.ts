import "rxjs/add/operator/map";
import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";
import { IConnector } from "./iconnector";
import { Promise, PromiseFactory } from "./../models/promise";
import { JsonHeaders } from "./jsonHeaders";
import { IEventManager } from "../event";
import { LoadingIndicatorEvent } from "../enum";
import { ValidationException, ValidationEvent } from "../exception";
import { HttpStatusCode, HttpError } from "./enum";
import { IoCNames } from "../ioc/enum";
import appHelper from "./../application/appHelper";

export class HttpConnector implements IConnector {
    private static eventManager: IEventManager;
    constructor() {
        if (!HttpConnector.eventManager) {
            HttpConnector.eventManager = window.ioc.resolve(IoCNames.IEventManager);
        }
    }
    public getJSON(jsonPath: string) {
        HttpConnector.eventManager.publish(LoadingIndicatorEvent.Show);
        let def = PromiseFactory.create();
        let headers = new JsonHeaders();
        let http: Http = window.ioc.resolve(Http);
        http.get(jsonPath, { headers: headers })
            .map((response: any) => response.json())
            .subscribe((data: any) => { def.resolve(data); });
        return def;
    }

    public post(url: string, data: any): Promise {
        HttpConnector.eventManager.publish(LoadingIndicatorEvent.Show);
        let def = PromiseFactory.create();
        let headers = new JsonHeaders();
        let dataToSend = JSON.stringify(data);
        let http: Http = window.ioc.resolve(Http);
        http.post(url, dataToSend, { headers: headers })
            .map((response: any) => response.json())
            .subscribe(
            (data: any) => this.handleResponse(def, data),
            (exception: any) => this.handleException(def, exception)
            );
        return def;
    }

    public put(url: string, data: any): Promise {
        HttpConnector.eventManager.publish(LoadingIndicatorEvent.Show);
        let def = PromiseFactory.create();
        let headers = new JsonHeaders();
        let dataToSend = JSON.stringify(data);
        let http: Http = window.ioc.resolve(Http);
        http.put(url, dataToSend, { headers: headers })
            .map((response: any) => response.json())
            .subscribe(
            (data: any) => this.handleResponse(def, data),
            (exception: any) => this.handleException(def, exception)
            );
        return def;
    }

    public get(url: string): Promise {
        HttpConnector.eventManager.publish(LoadingIndicatorEvent.Show);
        let def = PromiseFactory.create();
        let headers = new JsonHeaders();
        let http: Http = window.ioc.resolve(Http);
        http.get(url, { headers: headers })
            .map((response: any) => response.json())
            .subscribe(
            (data: any) => this.handleResponse(def, data),
            (exception: any) => this.handleException(def, exception)
            );
        return def;
    }

    public delete(url: string): Promise {
        HttpConnector.eventManager.publish(LoadingIndicatorEvent.Show);
        let def = PromiseFactory.create();
        let headers = new JsonHeaders();
        let http: Http = window.ioc.resolve(Http);
        http.delete(url, { headers: headers })
            .map((response: any) => response.json())
            .subscribe(
            (data: any) => this.handleResponse(def, data),
            (exception: any) => this.handleException(def, exception)
            );
        return def;
    }

    private handleResponse(def: Promise, response: any): any {
        HttpConnector.eventManager.publish(LoadingIndicatorEvent.Hide);
        if (response.errors.length === 0) {
            def.resolve(response.data);
            return;
        }
        let validationEror: ValidationException = this.getValidationExceptionFromResponse(response.errors);
        HttpConnector.eventManager.publish(ValidationEvent.ValidationFail, validationEror);
        def.reject(response.errors);
    }
    private handleException(def: Promise, exception: any) {
        HttpConnector.eventManager.publish(LoadingIndicatorEvent.Hide);
        let error: ValidationException = this.getError(exception);
        def.reject(error);
        HttpConnector.eventManager.publish(ValidationEvent.ValidationFail, error);
    }
    private getValidationExceptionFromResponse(responseErrors: Array<any>) {
        let validationEror: ValidationException = new ValidationException();
        responseErrors.forEach(function (errorItem: any) {
            validationEror.add(errorItem.key, errorItem.params);
        });
        return validationEror;
    }
    private getError(exception: any): ValidationException {
        let validationEror: ValidationException;
        switch (exception.status) {
            case HttpStatusCode.BadRequest:
                validationEror = exception.json().errors && exception.json().errors.length ?
                    this.getValidationExceptionFromResponse(exception.json().errors) :
                    new ValidationException(HttpError.BadRequest);
                break;
            case HttpStatusCode.NotFound:
                validationEror = new ValidationException(HttpError.NotFound);
                break;
            case HttpStatusCode.UnAuthorized:
                validationEror = new ValidationException(HttpError.UnAuthorized);
                break;
            default:
                validationEror = new ValidationException(HttpError.GenericError);
                break;
        }
        return validationEror;
    }
}