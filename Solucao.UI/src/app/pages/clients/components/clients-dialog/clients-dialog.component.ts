import { AfterViewInit, Component, ElementRef, Inject, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSelect } from '@angular/material/select';
import { ToastrService } from 'ngx-toastr';
import { City } from 'src/app/shared/models/city';
import { State } from 'src/app/shared/models/state';
import { EstadosCidadesServices } from 'src/app/shared/services/estados-cidades.service';
import { Client } from '../../../../shared/models/client';
import { ClientsService } from '../../../../shared/services/clients.service';

@Component({
    selector: 'app-clients-dialog',
    templateUrl: 'clients-dialog.component.html',
    styleUrls: ['./clients-dialog.component.scss']
  })
  export class ClientsDialogComponent implements OnInit, AfterViewInit {
    form: FormGroup;
    isAddMode: boolean;
    id: string;
    isPhysicalPerson: boolean;
    estados: State[];
    cidades: City[];
    @ViewChild('estado') selectEstado: MatSelect;
    @ViewChild('cidade') selectCidade: MatSelect;

    constructor(
      public dialogRef: MatDialogRef<ClientsDialogComponent>,
      @Inject(MAT_DIALOG_DATA) public data: any,
      private clientService: ClientsService,
      private formBuilder: FormBuilder,
      private toastr: ToastrService,
      private estadosCidadesService: EstadosCidadesServices) {
    }

    ngAfterViewInit(): void {
      if (this.data.element?.cityId != null){
        setTimeout(() => {
          this.selectEstado.options.filter(item => item.value == this.data.element?.stateId)[0].select();
          this.ajustesCSS();
        },500);
      }   
    }

    ngOnInit(): void {
      this.getEstados();
      this.id = this.data.element;
      this.isAddMode = !this.id;
      this.form = this.formBuilder.group({
        id:  [this.data.element?.id || ''],
        name: [this.data.element?.name || '', Validators.required],
        responsible: [this.data.element?.responsible || '', Validators.required],
        specialty: [this.data.element?.specialty || '', Validators.required],
        clinicName: [this.data.element?.clinicName || '', Validators.required],
        landMark: [this.data.element?.landMark || ''],
        zipCode: [this.data.element?.zipCode || '', Validators.required],
        neighborhood: [this.data.element?.neighborhood || '', Validators.required],
        cellPhone: [this.data.element?.cellPhone || '', Validators.required],
        clinicCellPhone: [this.data.element?.clinicCellPhone || ''],
        phone: [this.data.element?.phone || '', Validators.required],
        active: [this.data.element?.active || true, Validators.required],
        createdAt: [this.data.element?.createdAt || new Date()],
        updatedAt: [this.data.element?.updatedsAt || null],
        email: [this.data.element?.email || null],
        address: [this.data.element?.address || '', Validators.required],
        number: [this.data.element?.number || '', Validators.required],
        cpfCnpj: [this.data.element?.cpfCnpj || '', Validators.required],
        rgIe: [this.data.element?.rgIe || '', Validators.required],
        complement: [this.data.element?.complement || ''],
        cityId: [this.data.element?.cityId || '', Validators.required],
        stateId: [this.data.element?.stateId || '', Validators.required],
        isAnnualContract: [this.data.element?.isAnnualContract || '', Validators.required],
        isReceipt: [this.data.element?.isReceipt || 0],
        hasAirConditioning: [this.data.element?.hasAirConditioning || false],
        nameForReceipt: [this.data.element?.nameForReceipt || ''],
        takeTransformer: [this.data.element?.takeTransformer || false],
        has220V: [this.data.element?.has220V || false],
        hasStairs: [this.data.element?.hasStairs || false],
        hasTechnique: [this.data.element?.hasTechnique || false],
        techniqueOption1: [this.data.element?.techniqueOption1 || ''],
        techniqueOption2: [this.data.element?.techniqueOption2 || ''],
      });
      this.isPhysicalPerson = this.data.element? this.data.element.isPhysicalPerson : false;
    }

    onNoClick(): void {
      this.dialogRef.close();
    }

    getEstados(){
      this.estadosCidadesService.getEstados().subscribe((resp: State[]) => {
        this.estados = resp;
      });
    }

    getCidadesByEstado(estado_id: number){
      this.estadosCidadesService.getCidadesByEstado(estado_id).subscribe((resp: City[]) => {
        this.cidades = resp;
      });
      if (this.data.element?.cityId != null){
        setTimeout(() => {
          this.selectCidade.options.filter(item => item.value == this.data.element?.cityId)[0].select();
        },500);
      }
      
    }

    onSubmit(){
      if (this.form.value.id === ""){
        this.clientService.save(this.form.value).subscribe((resp: Client) => {
          this.toastr.success('Cliente adicionado.');
          this.dialogRef.close(resp);
        });
      } else {
        this.clientService.update(this.form.value).subscribe((resp: Client) => {
          this.toastr.success('Cliente atualizado!');
          this.dialogRef.close(resp);
        });
      }
    }

    ajustesCSS(){
      var mat_select = document.getElementsByClassName('mat-select');
      for (var i = 0; i < mat_select.length; i++) {
        mat_select[i].setAttribute('style', 'display: contents');
      }
    }
  }