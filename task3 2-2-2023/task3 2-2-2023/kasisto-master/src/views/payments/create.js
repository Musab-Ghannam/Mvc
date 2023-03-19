import Big from 'big.js'

import {
  func,
  number,
  shape,
  string
} from 'prop-types'
import React, { Component, Fragment } from 'react'
import { Redirect } from 'react-router-dom'
import { reduxForm } from 'redux-form'

import { amountType } from './utils'

import CancelPayment from '../../components/CancelPayment'

import DualCurrency from './dual-currency'

class CreatePayment extends Component {
  hasConnection = () => {
    const {
      address,
      height,
      paymentId,
      rate
    } = this.props.payment
    return !!(address && height && paymentId && rate)
  }

  isReady = () => {
    const requestedAmount = new Big(this.props.requestedAmount || 0)
    return requestedAmount.gt(0) && this.hasConnection()
  }

  render () {
    if (this.props.payment == null) {
      return <Redirect to='/' />
    }
    const {
      handleSubmit,
      payment: {
        address,
        fiatCurrency,
        rate
      }
    } = this.props

    const isStagenet = address != null && !address.startsWith('4')

    const isReady = this.isReady()

    return (
      <Fragment>
        <div className='o-app__content'>
          <form id='request-payment' onSubmit={handleSubmit}>
            <div className='o-content'>
              <h3 className='u-margin-bottom-none'>
                <label htmlFor='requestedAmount'>Amount due</label>
              </h3>
              <DualCurrency rate={rate} fiatCurrency={fiatCurrency} autoFocus />
            </div>
          </form>
        </div>
        <CancelPayment />
        <button form='request-payment' className='o-app__footer c-btn' disabled={!isReady}>
          { this.hasConnection() ? 'Request payment' : 'Waiting for wallet...' }
        </button>
        { isStagenet ? <small className='o-app__header u-brand-primary'>Stagenet</small> : null }
      </Fragment>
    )
  }

  static propTypes = {
    onSubmit: func.isRequired,
    payment: shape({
      exchange: string,
      rate: number,
      receipt: string,
      requestedAmount: amountType,
      convertedAmount: amountType
    }).isRequired
  }
}

const createForm = reduxForm({
  form: 'createPayment'
})

export default createForm(CreatePayment)
