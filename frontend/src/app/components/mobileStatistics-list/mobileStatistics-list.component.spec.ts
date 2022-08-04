import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MobileStatisticsListComponent } from './mobileStatistics-list.component';

describe('MobileStatisticsListComponent', () => {
  let component: MobileStatisticsListComponent;
  let fixture: ComponentFixture<MobileStatisticsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MobileStatisticsListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MobileStatisticsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
