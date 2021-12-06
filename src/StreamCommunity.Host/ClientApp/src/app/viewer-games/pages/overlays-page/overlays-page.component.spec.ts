import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OverlaysPageComponent } from './overlays-page.component';

describe('OverlaysPageComponent', () => {
  let component: OverlaysPageComponent;
  let fixture: ComponentFixture<OverlaysPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OverlaysPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OverlaysPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
