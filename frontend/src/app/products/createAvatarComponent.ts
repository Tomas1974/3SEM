import {Component} from "@angular/core";
import {FormBuilder, FormControl, Validators} from "@angular/forms";
import {Avatar} from "./products.service";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {firstValueFrom} from "rxjs";
import {State} from "../../state";
import {ModalController, ToastController} from "@ionic/angular";


@Component({
  template:
  `
    <ion-list>
      <ion-item>
        <ion-input [formControl]="createNewAvatarForm.controls.avatar_name" label="Insert title for avatar please">
        </ion-input>
        <div *ngIf="!createNewAvatarForm.controls.avatar_name.valid">Avatar name must be 3 characters long</div>
      </ion-item>
    </ion-list>

    <ion-list>
      <ion-item>
        <ion-input [formControl]="createNewAvatarForm.controls.avatar_price" type="number" label="Insert price for avatar please">
        </ion-input>
        <div *ngIf="!createNewAvatarForm.controls.avatar_price.valid">Avatar price must be above 1</div>
      </ion-item>
    </ion-list>

    <ion-list>
      <ion-item>
        <ion-input [formControl]="createNewAvatarForm.controls.information" label="Insert information for avatar">
        </ion-input>
      </ion-item>
    </ion-list>

    <ion-button [disabled]="createNewAvatarForm.invalid" (click)="submit()">Create new Avatar</ion-button>
  `
})
export class createAvatarComponent{
  avatar_name = new FormControl('', [Validators.minLength(3), Validators.required])
  avatar_price = new FormControl(0, [Validators.min(1), Validators.required])
  information = new FormControl('')

  createNewAvatarForm = this.fb.group({
    avatar_name: this.avatar_name,
    avatar_price: this.avatar_price,
    information: this.information
  })

  constructor(public fb: FormBuilder, public http: HttpClient, public state:State, public toastController: ToastController, public modalController: ModalController) {
  }

  async submit(){
    try{
      const observable = this.http.post<Avatar>(environment.baseUrl + '/avatar', this.createNewAvatarForm.value)
      const response = await firstValueFrom(observable)
      this.state.avatar.push(response!);
      const toast = await this.toastController.create({
        message: 'The avatar was successfully created',
        duration: 1233,
        color: "success"
      })
      toast.present();
      this.modalController.dismiss()
    } catch (e) {
      const toast = await this.toastController.create({
        message: 'The creation of the avatar was unsuccessful',
        duration: 1233,
        color: "danger"
      })
      toast.present();
    }
  }
}
