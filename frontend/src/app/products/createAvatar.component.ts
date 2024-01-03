import {Component} from "@angular/core";
import {FormBuilder, FormControl, Validators} from "@angular/forms";
import {Avatar, ResponseDto} from "./products.service";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {firstValueFrom} from "rxjs";
import {State} from "../../state";
import {ModalController, ToastController} from "@ionic/angular";
import {Router} from "@angular/router";


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
        <ion-textarea class="ion-text-wrap" [formControl]="createNewAvatarForm.controls.information" label="Insert information for avatar">
        </ion-textarea>
      </ion-item>
    </ion-list>

    <ion-button [disabled]="createNewAvatarForm.invalid" (click)="submit()">Create new Avatar</ion-button>
  `
})
export class CreateAvatarComponent{
  avatar_name = new FormControl('', [Validators.minLength(3), Validators.required])
  avatar_price = new FormControl(0, [Validators.min(1), Validators.required])
  information = new FormControl('')

  createNewAvatarForm = this.fb.group({
    avatar_name: this.avatar_name,
    avatar_price: this.avatar_price,
    information: this.information
  })

  constructor(public fb: FormBuilder, public https: HttpClient, public state:State, public toastController: ToastController, public modalController: ModalController, private readonly router: Router) {
  }
  observable: any;
  response: any;

  async submit(){
    try {
      let obs = this.https.post<ResponseDto<Avatar>>(environment.baseUrl + '/avatar', this.createNewAvatarForm.value)
      var response = await firstValueFrom<ResponseDto<Avatar>>(obs)
      this.state.avatar.push(response.responseData);
      const toast = await this.toastController.create({
        message: response.messageToClient,
        duration: 1233,
        color: "success"
      })
      toast.present();
    } catch (e){

    }
    finally{
      this.modalController.dismiss()
      }
  }
}
