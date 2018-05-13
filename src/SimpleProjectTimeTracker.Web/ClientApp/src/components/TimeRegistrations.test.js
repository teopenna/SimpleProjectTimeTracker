import React from 'react';
import { render, Simulate, fireEvent, wait } from 'react-testing-library';
import { MemoryRouter } from 'react-router';
import { createMemoryHistory } from 'history';
import TimeRegistrations from './TimeRegistrations';
import { apiConfiguration } from '../configuration';
import { fetchAllTimeRegistrations, deleteTimeRegistration } from '../services/TimeRegistrationService';
import { createInvoices } from '../services/InvoiceService';
import fetchMock from 'fetch-mock';

const fakeTimeRegistrations = [
    {
        id: 1,
        projectName: 'Project1',
        customerName: 'Customer1',
        date: new Date(),
        hoursWorked: 7.5,
        accounted: false
    }, {
        id: 2,
        projectName: 'Project2',
        customerName: 'Customer2',
        date: new Date(2018, 4, 8),
        hoursWorked: 7.5,
        accounted: true
    }
];

const setup = async() => {
    const history = createMemoryHistory('/');
    const renderedComponent = render(
        <MemoryRouter initialEntries={[ '/timeregistrations', '/invoices' ]}>
            <TimeRegistrations fetchAllTimeRegistrations={fetchAllTimeRegistrations} 
                deleteTimeRegistration={deleteTimeRegistration} 
                createInvoices={createInvoices}
                history={history}
            />
        </MemoryRouter>
    );

    await wait();

    return renderedComponent;
}

describe('Time Registrations Component', () => {
    beforeEach(() => {
        fetchMock.restore();

        fetchMock.getOnce(`${apiConfiguration.url}timeregistrations`, fakeTimeRegistrations);
        fetchMock.deleteOnce(`${apiConfiguration.url}timeregistrations/1`, fakeTimeRegistrations[0]);
        fetchMock.postOnce(`${apiConfiguration.url}invoices`, {});
    });

    it('should render displaying "Time Registrations" in page title', async() => {
        const { queryByText } = await setup();

        const title = queryByText('Time Registrations');
        expect(title.innerHTML).toBe('Time Registrations');
    });

    it('should render table of time registrations with columns "Project", "Customer", "Date", "Hours Worked", "Accounted" and "Edit/Delete"', async() => {
        const { getByTestId } = await setup();

        const timeRegistrationTable = getByTestId('timeregistrationtable');
        const tableHeaders = timeRegistrationTable.getElementsByTagName('th');
        expect(tableHeaders).toHaveLength(6);
        expect(tableHeaders[0].innerHTML).toBe('Project');
        expect(tableHeaders[1].innerHTML).toBe('Customer');
        expect(tableHeaders[2].innerHTML).toBe('Date');
        expect(tableHeaders[3].innerHTML).toBe('Hours Worked');
        expect(tableHeaders[4].innerHTML).toBe('Accounted');
        expect(tableHeaders[5].innerHTML).toBe('Edit/Delete');
    });

    it('should render existing time registrations', async() => {
        const { getByTestId } = await setup();

        const timeRegistrationTable = getByTestId('timeregistrationtable');
        const timeRegistrationTableBody = timeRegistrationTable.getElementsByTagName('tbody');

        expect(timeRegistrationTableBody).toHaveLength(1);
        expect(timeRegistrationTableBody[0].rows).toHaveLength(2);

        const timeRegistrationFirstTableRow = timeRegistrationTableBody[0].rows[0];
        const timeRegistrationTableFirstRowColumns = timeRegistrationFirstTableRow.getElementsByTagName('td');

        expect(timeRegistrationTableFirstRowColumns).toHaveLength(6);
        expect(timeRegistrationTableFirstRowColumns[0].innerHTML).toBe('Project1');
        expect(timeRegistrationTableFirstRowColumns[1].innerHTML).toBe('Customer1');
    });

    it('should create invoices when "Create invoices for not accounted items" button is clicked', async() => {
        const { getByText } = await setup();

        const createInvoicesButton = getByText('Create invoices for not accounted items');
        expect(createInvoicesButton.tagName.toLowerCase()).toBe('button');

        Simulate.click(createInvoicesButton);

        await wait(() => getByText('Create invoices for not accounted items'));

        expect(fetchMock.lastCall()[0]).toBe(`${apiConfiguration.url}invoices`);
    });

    it('should delete current time registration when button Delete is clicked', async() => {
        const { getByText, getByTestId } = await setup();

        const deleteButton = getByText('Delete');
        
        Simulate.click(deleteButton);

        await wait(() => getByText('Create invoices for not accounted items'));

        expect(fetchMock.lastCall()[0]).toBe(`${apiConfiguration.url}timeregistrations/1`);
        expect(fetchMock.lastCall()[1].method).toBe('DELETE');

        const timeRegistrationTable = getByTestId('timeregistrationtable');
        const timeRegistrationTableBody = timeRegistrationTable.getElementsByTagName('tbody');

        expect(timeRegistrationTableBody).toHaveLength(1);
        expect(timeRegistrationTableBody[0].rows).toHaveLength(1);
    });
});
