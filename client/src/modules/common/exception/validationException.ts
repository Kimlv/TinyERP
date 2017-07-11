import { ValidationError } from "./validationError";
import { ValidationEvent } from "./enum";
import { IoCNames } from "../ioc/enum";
export class ValidationException {
    constructor(key: string = "", params: any = {}) {
        if (key !== String.empty) {
            this.add(key, params);
        }
    }
    public errors: Array<ValidationError> = [];
    public add(key: string, params: any = {}): any {
        this.errors.push(new ValidationError(key, params));
    }
    public hasError(): boolean {
        return this.errors.length > 0;
    }
    public throwIfHasError(): void {
        window.ioc.resolve(IoCNames.IEventManager).publish(ValidationEvent.ValidationFail, this);
    }
}