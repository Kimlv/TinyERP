import { Component, Input, Output, EventEmitter } from "@angular/core";
@Component({
    selector: "form-button",
    templateUrl: "src/modules/common/components/form/formButton.html"
})
export class FormButton {
    @Input() label: string = "";
    @Input() cls: string = "bt-default";
    @Output() onClick: EventEmitter<any> = new EventEmitter<any>();
    public onClicked(event: any) {
        this.onClick.emit(event);
    }
}