import { Component } from "@angular/core";
import { FormBuilder, Validators } from "@angular/forms";
import {firstValueFrom} from "rxjs";
import { ToastController } from "@ionic/angular";
import { CustomValidators } from "../custom-validators";
import {AccountService, ResponseDto} from "./account.service";
import {environment} from "../../environments/environment.prod";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";

@Component({
  template: `
    <app-title title="Register"></app-title>
    <ion-content>
      <form [formGroup]="form" (ngSubmit)="submit()">
        <ion-list>

          <ion-item>
            <ion-input formControlName="full_name" data-testid="full_nameInput" placeholder="Your full name"
                       label-placement="floating">
              <div slot="label">Name
                <ion-text *ngIf="full_name.touched && full_name.invalid" color="danger">
                  Required
                </ion-text>
              </div>
            </ion-input>
          </ion-item>

          <ion-item>
            <ion-input formControlName="street" data-testid="addressInput" placeholder="Your address"
                       label-placement="floating">
              <div slot="label">Address
                <ion-text *ngIf="street.touched && street.invalid" color="danger">
                  Required
                </ion-text>
              </div>
            </ion-input>
          </ion-item>

          <ion-item>
            <ion-input type="number" formControlName="zip" data-testid="zipcodeInput" placeholder="Your Zip-code"
                       label-placement="floating">
              <div slot="label">Zic code
                <ion-text *ngIf="zip.touched && zip.invalid" color="danger">
                  Required
                </ion-text>
              </div>
            </ion-input>
          </ion-item>

          <ion-item>
            <ion-input formControlName="email" data-testid="emailInput" placeholder="Email (also used for login)"
                       label-placement="floating">
              <div slot="label">Email
                <ion-text *ngIf="email.touched && email.invalid" color="danger">Valid
                  email is required
                </ion-text>
              </div>
            </ion-input>
          </ion-item>

          <ion-item>
            <ion-input type="password" formControlName="password" data-testid="passwordInput"
                       placeholder="Type a hard to guess password" label-placement="floating">
              <div slot="label">Password
                <ion-text *ngIf="password.touched && password.errors?.['required']" color="danger">
                  Required
                </ion-text>
                <ion-text *ngIf="password.touched && password.errors?.['minlength']" color="danger">
                  Too short
                </ion-text>
              </div>
            </ion-input>
          </ion-item>

          <ion-item>
            <ion-input type="password" formControlName="passwordRepeat" data-testid="passwordRepeatInput"
                       placeholder="Repeat your password to make sure it was typed correct" label-placement="floating">
              <div slot="label">Password (again)
                <ion-text *ngIf="passwordRepeat.touched && passwordRepeat.errors?.['matchOther']" color="danger">
                  Must match the password
                </ion-text>
              </div>
            </ion-input>
          </ion-item>

        </ion-list>

        <ion-button id="btn-submit" [disabled]="form.invalid" (click)="submit()">Submit</ion-button>
      </form>
    </ion-content>
  `,
  styleUrls: ['./form.css'],
})


export class RegisterComponent {
  form = this.fb.group({
    full_name: ['', Validators.required],
    street: ['', Validators.required],
    zip: [0, Validators.required],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(8)]],
    passwordRepeat: ['', [Validators.required, CustomValidators.matchOther('password')]]
  });


  get full_name() {
    return this.form.controls.full_name;
  }

  get street() {
    return this.form.controls.street;
  }

  get zip() {
    return this.form.controls.zip;
  }

  get email() {
    return this.form.controls.email;
  }

  get password() {
    return this.form.controls.password
  }

  get passwordRepeat() {
    return this.form.controls.passwordRepeat
  }


  constructor(
    private readonly http: HttpClient,
    private readonly fb: FormBuilder,
    private readonly service: AccountService,
    private readonly toast: ToastController
  ) {
  }


  async submit() {
    try {
      if (this.form.invalid) return;
      await firstValueFrom(this.http.post<ResponseDto<any>>(environment.baseUrl + '/api/account/register', this.form.value));

      (await this.toast.create({
        message: "Thank you for signing up!",
        color: "success",
        duration: 5000
      })).present();
    } catch (e) {
      (await this.toast.create({
      message: (e as HttpErrorResponse).error.messageToClient,
        color :"danger",
        duration: 5000
    })).present();
    }
  }
}
